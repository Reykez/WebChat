import React from 'react';

import { SendMessageForm } from './SendMessageForm';
import { MessageContainer } from './MessageContainer';
import { UserList } from './UserList';
import { Button } from 'react-bootstrap';
import './ChatPanel.css';

export const ChatPanel = ({sendMessage, messages, users, receiver, logOut, sender, changeReceiver}) => {
    return (
        <div className="chat-panel">
            <UserList users={users} sender={sender} receiver={receiver} changeChat={changeReceiver}/>
            <div className="top-nav-bar">
                <h2 className="element">
                    {sender}
                </h2>
                <div className="element">
                    <Button variant='danger' onClick={() => logOut()}>Logout</Button>
                </div>
            </div>
            <MessageContainer messages={messages} room={receiver}/>
            {receiver != "none" ? (<SendMessageForm sendMessage={sendMessage} mReceiver={receiver}/>) : (<div className="send-message-mock"></div>) }
        </div>
    );
}
