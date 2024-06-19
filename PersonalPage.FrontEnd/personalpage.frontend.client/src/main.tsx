import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import Intro from './Components/Intro/Intro';
import Experience from './Components/Experience/Experience';
import Education from './Components/Education/Education';
import Footer from './Components/Footer/Footer.tsx';
import Skills from './Components/Skills/Skills.tsx';

const leetCodeLink = 'https://leetcode.com/u/Jakkellss/';
const linkedInLink = 'https://www.linkedin.com/in/jacoellis/';
const githubLink = 'https://github.com/Jakkells';
const name = 'Jaco Ellis';
const phoneNumber = '+27 064 859 8349';
const email = 'jacoellis222@gmail.com';

ReactDOM.createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <Intro />
        <Experience />
        <Education />
        <Skills />
        <Footer
            leetCodeLink={leetCodeLink}
            linkedInLink={linkedInLink}
            githubLink={githubLink}
            name={name}
            phoneNumber={phoneNumber}
            email={email}
        />
    </React.StrictMode>
);

