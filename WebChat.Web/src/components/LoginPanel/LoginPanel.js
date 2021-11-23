import {React, useState} from 'react'
import {Form, Button} from 'react-bootstrap';
import { tsParticles } from 'tsparticles';
import './LoginPanel.css';

export const LoginPanel = ({connect, setOnlyRed}) => {
    const [username, setUsername] = useState();
    const [password, setPassword] = useState();

    function timeout(delay) {
        return new Promise(res => setTimeout(res, delay));
    }

    const handleSubmit = async () => {
        console.log(`Username: ${username}, password: ${password}`);

        var token = await getFetch();
        if(await token.status === 200) {
            var tokenText = await token.text();
            console.log(`Success, token: ${tokenText}`);
            var connectResult = await connect(username, tokenText);
        }
        else {
            const particles = tsParticles.domItem(0);
            const emitterNormal = particles.plugins.get("emitters").array[0];
            const emitterRed = particles.plugins.get("emitters").array[1];

            emitterNormal.pause();
            await timeout(200);
            emitterRed.emitterOptions.rate.quantity = 9;
            emitterRed.play();

            await timeout(5500);
            emitterRed.pause();
            emitterRed.emitterOptions.rate.quantity = 0;
            await timeout(1500);
            emitterNormal.play();
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