import React from 'react';
import { useState } from 'react';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { LoginPanel } from './components/LoginPanel/LoginPanel.js';
import { ChatPanel } from './components/ChatPanel/ChatPanel.js';
import { ParticlesContainer } from './components/LoginPanel/ParticlesContainer.js';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
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

            return true;
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

    const getMessages = async(receiver) => {
        try {
            setMessages([]);
            console.log(`receiver: ${receiver}`)
            await connection.invoke("GetMessages", receiver);
        } catch (e) {
            console.log(e);
        }
    }

    document.body.style= 'background: #030518;';
    
    return (
        <div>
            {!connection
                ? <div className="App"> 
                    <div className="graphic"><ParticlesContainer /> </div>
                    <div className="content"><LoginPanel connect={connect}/></div>
                  </div>
                : <ChatPanel getMessages={getMessages} messages={messages}
                    sendMessage={sendMessage} users={users} logOut={logOut} 
                    sender={username} receiver={receiver} changeReceiver={setReceiver} />
            }
        </div>
    );
}

export default App;
