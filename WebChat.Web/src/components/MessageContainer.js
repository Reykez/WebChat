import { useEffect, useRef } from 'react';
import './MessageContainer.css';

export const MessageContainer = ({messages, room}) => {
    const messageRef = useRef();

    useEffect(() => {
        if(messageRef && messageRef.current) {
            const {scrollHeight, clientHeight} = messageRef.current;
            messageRef.current.scrollTo({left:0, top: scrollHeight-clientHeight, behavior: 'smooth'})
        }
    }, [messages]);

    return (
        <div ref={messageRef} className="message-container">
            {messages.map((m, index) =>
                {
                    if(m.sender == room || m.receiver == room) {
                        return (
                            <div key={index} className="user-message">
                                <div className="message-text">{m.message}</div>
                                <div className="from-user">{m.sender} - {m.timestamp}</div>
                            </div>  
                        ) 
                    }
                }
            )}
        </div>
    )
}