import { useEffect, useRef } from 'react';
import './MessageContainer.css';

export const MessageContainer = ({messages, room, user}) => {
    const messageRef = useRef();

    useEffect(() => {
        if(messageRef && messageRef.current) {
            const {scrollHeight, clientHeight} = messageRef.current;
            messageRef.current.scrollTo({left:0, top: scrollHeight-clientHeight, behavior: 'smooth'})
        }
    }, [messages]);

    var isFirst = true;

    return (
        <div ref={messageRef} className="message-container">
            {messages.map((m, index) =>
                {
                    let classNames = ["user-message"];
                    if(isFirst) { isFirst = false; classNames.push("first"); }
                    if(m.sender === user) {
                        classNames.push("right")
                    }
                    

                    if(m.sender === room || m.receiver === room) {
                        return (
                            <div key={index} className={classNames.join(' ')}>
                                <div className="message-text">{m.message}</div>
                                <div className="from-user">{m.sender} - {m.timestamp}</div>
                            </div>   
                        )
                    }
                    return(null);
                }
            )}
        </div>
    )
}