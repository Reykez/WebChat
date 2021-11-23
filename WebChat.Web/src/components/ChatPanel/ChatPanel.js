import React from 'react';

import { SendMessageForm } from './SendMessageForm';
import { MessageContainer } from './MessageContainer';
import { UserList } from './UserList';
import { Button } from 'react-bootstrap';
import './ChatPanel.css';

export const ChatPanel = ({sendMessage, messages, users, receiver, logOut, sender, changeReceiver, getMessages}) => {
    return (
        <div className="chat-panel">
            <div className="left-nav-bar">
                <UserList users={users} sender={sender} receiver={receiver} changeChat={changeReceiver} getMessages={getMessages}/>
            </div>
            <div className="top-nav-bar">
                <h2 className="element">
                    {sender} &nbsp;
                    <Button className="logout-button" variant='danger' onClick={() => logOut()}>Logout</Button>
                </h2>
                <h2 className="element">
                    {receiver !== "none" ? receiver : <div></div>}
                </h2>
            </div>
            <div className="message-box">
                <MessageContainer messages={messages} room={receiver} user={sender}/>
                {receiver !== "none" ? (<SendMessageForm sendMessage={sendMessage} mReceiver={receiver}/>) : (<div className="send-message-mock"></div>) }
            </div>
        </div>
    );
}
