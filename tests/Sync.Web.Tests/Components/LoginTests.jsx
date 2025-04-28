import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import Login from '../../src/components/Auth/Login';
import apiService from '../../src/services/apiService';

jest.mock('../../src/services/apiService');

describe('Login Component', () => {
    test('renders login form', () => {
        render(<Login />);
        expect(screen.getByPlaceholderText('Email')).toBeInTheDocument();
        expect(screen.getByPlaceholderText('Password')).toBeInTheDocument();
    });

    test('submits form with valid data', async () => {
        apiService.login.mockResolvedValue({ token: 'fake-token' });
        render(<Login />);
        fireEvent.change(screen.getByPlaceholderText('Email'), { target: { value: 'test@example.com' } });
        fireEvent.change(screen.getByPlaceholderText('Password'), { target: { value: 'password' } });
        fireEvent.click(screen.getByText('Login'));
        expect(apiService.login).toHaveBeenCalledWith({ email: 'test@example.com', password: 'password' });
    });
});