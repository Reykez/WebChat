import React from 'react';
import './App.css';
import { useState } from 'react';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import 'bootstrap/dist/css/bootstrap.min.css';

import { LoginPanel } from './components/LoginPanel.js';
import { ChatPanel } from './components/ChatPanel.js';

function App() {
    const [connection, setConnection] = useState();
    const [messages, setMessages] = useState([]);
    const [users, setUsers] = useState([]);

    const [username, setUsername] = useState("none");
    const [receiver, setReceiver] = useState("none");

    const connect = async (sender, token) => {
        try {
            const con = new HubConnectionBuilder()
                .withUrl("https://localhost:5001/chat", {
                    accessTokenFactory: () => token
                })
                .configureLogging(LogLevel.Information)
                .build();

            con.on("ReceiveMessage", (sender, receiver, message, timestamp) => {
                setMessages(messages => [...messages, { sender, receiver, message, timestamp}]);
            });

            con.on("AvailableUsers", (users) => {
                setUsers(users);
            });

            con.onclose(e => {
                setConnection();
                setMessages([]);
                setUsers([]);
            });

            await con.start();
            await con.invoke("JoinRoom", { sender }); // JoinRoom Username

            setUsername(sender);
            setReceiver(receiver);
            setConnection(con);
        } catch (e) {
            console.log(e);
        }
    }

    const sendMessage = async (message, receiver) => {
        try {
            await connection.invoke("SendMessage", { message, receiver });
        } catch (e) {
            console.log(e);
        }
    }

    const logOut = async () => {
        try {
            await connection.stop();
            setUsers([]);
            setUsername("none");
            setReceiver("none");
        } catch (e) {
            console.log(e);
        }
    }

    return (
        <div className="App">
            {!connection
                ? <LoginPanel connect={connect} />
                : <ChatPanel sendMessage={sendMessage} messages={messages} users={users} logOut={logOut} sender={username} receiver={receiver} changeReceiver={setReceiver} connect={connect} />
            }
        </div>
    );
}

export default App;
