import React, { useState, useEffect } from 'react';
import apiService from '../../services/apiService';
import encryptionService from '../../services/encryptionService';

const ChatWindow = ({ chatId }) => {
    const [messages, setMessages] = useState([]);
    const [newMessage, setNewMessage] = useState('');
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchMessages = async () => {
            try {
                const response = await apiService.getMessages(chatId);
                const decryptedMessages = response.map(msg => ({
                    ...msg,
                    content: encryptionService.decrypt(msg.content, 'your-encryption-key')
                }));
                setMessages(decryptedMessages);
            } catch (err) {
                setError('Failed to load messages');
            }
        };
        fetchMessages();
    }, [chatId]);

    const handleSendMessage = async () => {
        try {
            const encryptedContent = encryptionService.encrypt(newMessage, 'your-encryption-key');
            await apiService.sendMessage(chatId, { content: encryptedContent });
            setNewMessage('');
            const response = await apiService.getMessages(chatId);
            const decryptedMessages = response.map(msg => ({
                ...msg,
                content: encryptionService.decrypt(msg.content, 'your-encryption-key')
            }));
            setMessages(decryptedMessages);
        } catch (err) {
            setError('Failed to send message');
        }
    };

    return (
        <div className="p-4">
            <h2 className="text-xl font-bold mb-4">Chat</h2>
            {error && <p className="text-red-500">{error}</p>}
            <div className="h-96 overflow-y-auto mb-4">
                {messages.map(msg => (
                    <div key={msg.id} className="p-2">
                        <strong>{msg.senderId}</strong>: {msg.content}
                    </div>
                ))}
            </div>
            <div className="flex">
                <input
                    type="text"
                    value={newMessage}
                    onChange={(e) => setNewMessage(e.target.value)}
                    className="flex-1 p-2 border rounded"
                />
                <button
                    onClick={handleSendMessage}
                    className="p-2 bg-blue-500 text-white rounded"
                >
                    Send
                </button>
            </div>
        </div>
    );
};

export default ChatWindow;