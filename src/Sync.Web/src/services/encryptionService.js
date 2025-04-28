import CryptoJS from 'crypto-js';

const encryptionService = {
    encrypt(text, key) {
        return CryptoJS.AES.encrypt(text, key).toString();
    },

    decrypt(cipherText, key) {
        const bytes = CryptoJS.AES.decrypt(cipherText, key);
        return bytes.toString(CryptoJS.enc.Utf8);
    }
};

export default encryptionService;