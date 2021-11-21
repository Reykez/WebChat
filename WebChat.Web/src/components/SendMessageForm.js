import {Form, Button, FormControl, InputGroup} from 'react-bootstrap';
import {useState} from 'react';

export const SendMessageForm = ({sendMessage, mReceiver}) => {
    const [message, setMessage] = useState('');

    return (
        <Form
            onSubmit={e => {
                e.preventDefault();
                sendMessage(message, mReceiver);
                setMessage('');
            }}>
            <InputGroup>
                <FormControl type="user" placeholder="type message" 
                    onChange={e => setMessage(e.target.value)} value={message} />
                    <Button variant="primary" type="submit" disabled={!message}>Send</Button>
            </InputGroup>
        </Form>
    )
}