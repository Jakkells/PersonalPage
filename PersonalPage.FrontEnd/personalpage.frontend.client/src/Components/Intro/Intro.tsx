import React from 'react';
import './Intro.css'; 

//React code for starting part of website with static text and image
function Intro() {
    return (
        <div className="intro-container">
            <table className="intro-table">
                <tbody>
                    <tr>
                        <td className="intro-content">
                            <h1>Jaco Ellis</h1>
                            <h2>Software Engineer</h2>
                            <br />
                            <h3>About</h3>
                            <p>I am a software engineer with a strong passion for continuous learning and innovation. I thrive on exploring new technologies and solving complex problems to create impactful software solutions. </p>
                        </td>
                        <td className="intro-content">
                            <img style={{width: '200%'}} src="https://media.licdn.com/dms/image/D4D35AQG7ASkBdHCgAQ/profile-framedphoto-shrink_400_400/0/1705917230397?e=1719313200&v=beta&t=lnsiroCqPVBvdEtLrFdWCrtPFR0YOAAkFjBGqjD_kIg" alt="Profile" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}

export default Intro;
