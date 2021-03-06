import './UserList.css';

export const UserList = ({users, changeChat, sender, receiver, getMessages}) => {

    const handleChatChange = (username) => {
        getMessages(username);
        changeChat(username);
    }

    return (
        <div className="user-list">
            <h4>Available Users</h4>
            {users.map((u, idx) =>
                {   if(u === receiver){return <h6 className="user-list-element selected" key={idx}>{u}</h6>}
                    else if(u !== sender){return <h6 className="user-list-element" key={idx} onClick={() => handleChatChange(u)}>{u}</h6>}
                    return null;
                }
            )}
        </div>
    )
}