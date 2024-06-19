// Footer.tsx
import React from 'react';

//Gets inputs from main.tsx
interface FooterProps {
    leetCodeLink: string;
    linkedInLink: string;
    githubLink: string;
    name: string;
    phoneNumber: string;
    email: string;
}

//React code for footer part of website with links and details.
const Footer: React.FC<FooterProps> = ({
    leetCodeLink,
    linkedInLink,
    githubLink,
    name,
    phoneNumber,
    email,
}) => {
    return (
        <footer style={{ backgroundColor: '#000', color: '#fff', padding: '20px', textAlign: 'center' }}>
            <div style={{ marginBottom: '10px' }}>
                <span style={{ fontWeight: 'bold' }}>{name}</span>
            </div>
            <div style={{ marginBottom: '10px' }}>
                <a href={leetCodeLink} target="_blank" rel="noopener noreferrer" style={{ color: '#fff', textDecoration: 'none', marginRight: '10px' }}>LeetCode</a>
                <a href={linkedInLink} target="_blank" rel="noopener noreferrer" style={{ color: '#fff', textDecoration: 'none', marginRight: '10px' }}>LinkedIn</a>
                <a href={githubLink} target="_blank" rel="noopener noreferrer" style={{ color: '#fff', textDecoration: 'none', marginRight: '10px' }}>GitHub</a>
            </div>
            <div>
                <span style={{ fontWeight: 'bold' }}>Contact:</span>
                <span style={{ marginLeft: '10px' }}>{phoneNumber}</span>
                <span style={{ marginLeft: '10px' }}>{email}</span>
            </div>
        </footer>
    );
};

export default Footer;
