import {Form, Button, FormControl, InputGroup} from 'react-bootstrap';
import {useState} from 'react';
import "./SendMessageForm.css";

export const SendMessageForm = ({sendMessage, mReceiver}) => {
    const [message, setMessage] = useState('');

    return (
        <Form
            onSubmit={e => {
                e.preventDefault();
                sendMessage(message, mReceiver);
                setMessage('');
            }}>
            <InputGroup className="input-group">
                <FormControl className="input-field" type="user" placeholder="Type message" 
                    onChange={e => setMessage(e.target.value)} value={message} />
                    <Button variant="primary" type="submit" disabled={!message}>Send</Button>
            </InputGroup>
        </Form>
    )
}