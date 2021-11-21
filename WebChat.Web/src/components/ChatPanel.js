import React from 'react';

import { SendMessageForm } from './SendMessageForm';
import { MessageContainer } from './MessageContainer';
import { UserList } from './UserList';

import { Button } from 'react-bootstrap';

export const ChatPanel = ({sendMessage, messages, users, receiver, logOut, sender, changeReceiver}) => {
    return (
        <div>
            <div className="leave-room">
                <Button variant='normal' onClick={() => logOut()}>Logout</Button>
                <Button variant="normal" onClick={() => changeReceiver()}>Receiver_D</Button>
            </div>
            <div className="room-name">
                You are (sender): {sender} <br/>
                Room name (receiver): {receiver}
            </div>
            <UserList users={users} sender={sender} receiver={receiver} changeChat={changeReceiver}/>
            <div className="chat">
                <MessageContainer messages={messages} room={receiver}/>
                <SendMessageForm sendMessage={sendMessage} mReceiver={receiver}/>
            </div>
        </div>
    );
}
