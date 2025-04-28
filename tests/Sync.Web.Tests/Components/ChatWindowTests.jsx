import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import ChatWindow from '../../src/components/Chat/ChatWindow';
import apiService from '../../src/services/apiService';
import encryptionService from '../../src/services/encryptionService';

jest.mock('../../src/services/apiService');
jest.mock('../../src/services/encryptionService');

describe('ChatWindow Component', () => {
    test('renders chat window', () => {
        render(<ChatWindow chatId="chat1" />);
        expect(screen.getByText('Chat')).toBeInTheDocument();
    });

    test('sends message', async () => {
        apiService.getMessages.mockResolvedValue([{ id: '1', content: 'test', senderId: 'user1' }]);
        encryptionService.encrypt.mockReturnValue('encrypted');
        encryptionService.decrypt.mockReturnValue('test');
        apiService.sendMessage.mockResolvedValue({});
        render(<ChatWindow chatId="chat1" />);
        fireEvent.change(screen.getByPlaceholderText('Type a message'), { target: { value: 'Hello' } });
        fireEvent.click(screen.getByText('Send'));
        expect(apiService.sendMessage).toHaveBeenCalledWith('chat1', { content: 'encrypted' });
    });
});