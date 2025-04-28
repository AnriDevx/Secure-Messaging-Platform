import React, { useState, useEffect } from 'react';
import apiService from '../../services/apiService';

const ChatList = () => {
    const [chats, setChats] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchChats = async () => {
            try {
                const response = await apiService.getChats();
                setChats(response);
            } catch (err) {
                setError('Failed to load chats');
            }
        };
        fetchChats();
    }, []);

    return (
        <div className="p-4">
            <h2 className="text-xl font-bold mb-4">Chats</h2>
            {error && <p className="text-red-500">{error}</p>}
            <ul>
                {chats.map(chat => (
                    <li key={chat.id} className="p-2 border-b">
                        <a href={`/chat/${chat.id}`}>{chat.username}</a>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ChatList;