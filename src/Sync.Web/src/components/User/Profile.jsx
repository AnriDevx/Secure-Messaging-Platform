import React, { useState, useEffect } from 'react';
import apiService from '../../services/apiService';

const Profile = () => {
    const [user, setUser] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchProfile = async () => {
            try {
                const response = await apiService.getCurrentUser();
                setUser(response);
            } catch (err) {
                setError('Failed to load profile');
            }
        };
        fetchProfile();
    }, []);

    const handleDeleteAccount = async () => {
        try {
            await apiService.deleteAccount();
            localStorage.removeItem('token');
            window.location.href = '/login';
        } catch (err) {
            setError('Failed to delete account');
        }
    };

    return (
        <div className="p-4">
            <h2 className="text-xl font-bold mb-4">Profile</h2>
            {error && <p className="text-red-500">{error}</p>}
            {user && (
                <div>
                    <p><strong>Username:</strong> {user.username}</p>
                    <p><strong>Email:</strong> {user.email}</p>
                    <button
                        onClick={handleDeleteAccount}
                        className="mt-4 p-2 bg-red-500 text-white rounded"
                    >
                        Delete Account
                    </button>
                </div>
            )}
        </div>
    );
};

export default Profile;