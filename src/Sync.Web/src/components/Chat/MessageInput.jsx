import React, { useState } from 'react';

const MessageInput = ({ onSend }) => {
    const [message, setMessage] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        if (message.trim()) {
            onSend(message);
            setMessage('');
        }
    };

    return (
        <div className="flex p-2">
            <input
                type="text"
                value={message}
                onChange={(e) => setMessage(e.target.value)}
                placeholder="Type a message"
                className="flex-1 p-2 border rounded"
            />
            <button
                onClick={handleSubmit}
                className="p-2 bg-blue-500 text-white rounded"
            >
                Send
            </button>
        </div>
    );
};

export default MessageInput;