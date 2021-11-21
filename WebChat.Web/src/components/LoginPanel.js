import {React, useState} from 'react'
import {Form, Button} from 'react-bootstrap';

export const LoginPanel = ({connect}) => {
    const [username, setUsername] = useState();
    const [password, setPassword] = useState();

    const handleSubmit = async () => {
        console.log(`Username: ${username}, password: ${password}`);

        var token = await getFetch();
        if(await token.status == 200) {
            var tokenText = await token.text();
            console.log(`Success, token: ${tokenText}`);
            connect(username, tokenText);
        }
        else {
            console.log(`Error :c `);
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
        //console.log(tok);
        //console.log(await tok.text());
        return fetchRequest;
    }

    return (
        <Form className="login-panel"
            onSubmit={e => {
                e.preventDefault();
                handleSubmit();
            }}>
            <Form.Group>
                <Form.Control placeholder="Username" onChange={e => setUsername(e.target.value)}/>
                <Form.Control placeholder="Password" onChange={e => setPassword(e.target.value)}/>
            </Form.Group>
            <Button variant="success" type="submit" disabled={!username || !password}>Join</Button>
        </Form>
    );
}