import {React, useState} from 'react'
import {Form, Button} from 'react-bootstrap';
import './LoginPanel.css';

export const LoginPanel = ({connect}) => {
    const [username, setUsername] = useState();
    const [password, setPassword] = useState();

    const handleSubmit = async () => {
        console.log(`Username: ${username}, password: ${password}`);

        var token = await getFetch();
        if(await token.status == 200) {
            var tokenText = await token.text();
            console.log(`Success, token: ${tokenText}`);
            var connectResult = await connect(username, tokenText);
        }
        else {
        }
    } 

    const getFetch = async () => {
        var fetchRequest = await fetch("https://localhost:5001/account/login", {
            method: 'POST',
            headers: {'Content-Type' : 'application/json'},
            credentials: 'include', 
            body: JSON.stringify({
                username,
                password
            }) 
        });
        return fetchRequest;
    }

    return (
        <div className="login-panel">
            <Form className="login-form d-grid gap-2"
                onSubmit={e => {
                    e.preventDefault();
                    handleSubmit();
                }}>
                <h2> Sign In </h2>
                <Form.Group>
                    <Form.Label> Username </Form.Label>
                    <Form.Control className="input-text" placeholder="Username" onChange={e => setUsername(e.target.value)}/>
                    <Form.Label> Password </Form.Label>
                    <Form.Control className="input-text" placeholder="Password" type="password" onChange={e => setPassword(e.target.value)}/>
                </Form.Group>
                <Button className="submit-button" variant="success" type="submit" size="lg" disabled={!username || !password}>Join</Button>
            </Form>       
        </div>
    );
}