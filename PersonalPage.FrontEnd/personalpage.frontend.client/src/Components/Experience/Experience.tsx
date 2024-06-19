import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './Modal.css';

interface ExperienceModel {
    id: number;
    company: string;
    title: string;
    description: string;
    startDate: Date;
    endDate: Date;
}

//Experience List for adding update and delete Work experience
const Experience: React.FC = () => {
    const [experiences, setExperiences] = useState<ExperienceModel[]>([]);
    const [newExperience, setNewExperience] = useState<ExperienceModel>({
        id: 0,
        company: '',
        title: '',
        description: '',
        startDate: new Date(),
        endDate: new Date(),
    });

    const [isEditing, setIsEditing] = useState(false); // State to track if editing
    const [editId, setEditId] = useState<number | null>(null); // State to track the id being edited

    // State for controlling modal visibility
    const [showModal, setShowModal] = useState(false);

    useEffect(() => {
        fetchExperiences();
    }, []);

    const fetchExperiences = async () => {
        try {
            const response = await axios.get<ExperienceModel[]>('https://localhost:7158/api/Experience');
            console.log('Response data:', response.data);

            // Ensure startDate and endDate are converted to Date objects
            const experiencesWithDates = response.data.map((experience) => ({
                ...experience,
                startDate: new Date(experience.startDate),
                endDate: new Date(experience.endDate)
            }));

            setExperiences(experiencesWithDates || []);
        } catch (error) {
            console.error('Error fetching experiences:', error);
        }
    };

    const handleAddExperience = async (event: React.FormEvent) => {
        event.preventDefault(); // Prevent default form submission

        try {
            if (isEditing) {
                // Update existing experience
                await axios.put(`https://localhost:7158/api/Experience/${editId}`, newExperience);
            } else {
                // Add new experience
                await axios.post('https://localhost:7158/api/Experience', newExperience);
            }

            console.log('Experience added/updated successfully.');

            setNewExperience({
                id: 0,
                company: '',
                title: '',
                description: '',
                startDate: new Date(),
                endDate: new Date(),
            });

            fetchExperiences(); // Refresh the list after adding or updating
            setShowModal(false); // Close the modal after successful addition or update
            setIsEditing(false); // Reset edit mode
            setEditId(null); // Clear edit id
        } catch (error) {
            console.error('Error adding/updating experience:', error);
        }
    };

    const handleDeleteExperience = async (id: number) => {
        try {
            await axios.delete(`https://localhost:7158/api/Experience/${id}`);
            fetchExperiences(); // Refresh the list after deletion
        } catch (error) {
            console.error('Error deleting experience:', error);
        }
    };

    const handleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = event.target;

        // For date fields, parse the date string into Date object
        if (name === 'startDate' || name === 'endDate') {
            setNewExperience(prevExperience => ({
                ...prevExperience,
                [name]: new Date(value), // Parse date string to Date object
            }));
        } else {
            setNewExperience(prevExperience => ({
                ...prevExperience,
                [name]: value,
            }));
        }
    };

    const openModalForAdd = () => {
        setShowModal(true);
        setIsEditing(false); // Ensure not in edit mode when adding
    };

    const openModalForEdit = (experience: ExperienceModel) => {
        setNewExperience(experience); // Populate form with selected experience
        setShowModal(true);
        setIsEditing(true); // Set edit mode
        setEditId(experience.id); // Set the id of the experience being edited
    };

    const closeModal = () => {
        setShowModal(false);
        setIsEditing(false); // Reset edit mode on modal close
        setEditId(null); // Clear edit id on modal close
    };

    return (
        <div>
            <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', marginBottom: '20px' }}>
                <h1 style={{ marginRight: '10px' }}>Experience</h1>
                <button style={{ marginTop: '15px' }}  onClick={openModalForAdd}>Add</button>
            </div>

            {/* Modal (box that pops up to input)*/}
            {showModal && (
                <div className="modal" >
                    <div className="modal-content" style={{ height: '300px' }}>
                        <span className="close" onClick={closeModal}>&times;</span>
                        <h2>{isEditing ? 'Edit Experience' : 'Add New Experience'}</h2>
                        <form onSubmit={handleAddExperience} style={{ display: 'flex', flexDirection: 'column' }}>
                            <table>
                                <tr>
                                    <td>
                                        <label style={{ marginBottom: '10px' }}>
                                            Company:
                                        </label>
                                    </td>
                                    <td>
                                        <input
                                            type="text"
                                            name="company"
                                            value={newExperience.company}
                                            onChange={handleChange}
                                            required
                                        />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    <label style={{ marginBottom: '10px' }}>
                                        Title:
                                    </label>
                                    </td>
                                    <td>
                                        <input
                                            type="text"
                                            name="title"
                                            value={newExperience.title}
                                            onChange={handleChange}
                                            required
                                        />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label style={{ marginBottom: '10px' }}>
                                            Description:
                                        </label>
                                    </td>
                                    <td>
                                        <textarea
                                            name="description"
                                            value={newExperience.description}
                                            onChange={handleChange}
                                            required
                                        />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label style={{ marginBottom: '10px' }}>
                                            Start Date:
                                        </label>
                                    </td>
                                    <td>
                                        <input
                                            type="date"
                                            name="startDate"
                                            value={newExperience.startDate.toISOString().split('T')[0]}
                                            onChange={handleChange}
                                            required
                                        />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label style={{ marginBottom: '10px' }}>
                                            End Date:
                                        </label>
                                    </td>
                                    <td>
                                        <input
                                            type="date"
                                            name="endDate"
                                            value={newExperience.endDate.toISOString().split('T')[0]}
                                            onChange={handleChange}
                                            required
                                        />
                                    </td>
                                </tr>
                            </table>                            
                            <button type="submit" style={{ marginTop: '10px', width: '100%' }}>
                                {isEditing ? 'Update Experience' : 'Add Experience'}
                            </button>
                        </form>
                    </div>
                </div>
            )}

            <div>
                {Array.isArray(experiences) &&
                    experiences.map((experience) => (
                        <div key={experience.id} style={{ marginBottom: '10px' }}>
                            <h3>{experience.company}</h3>
                            <h3>{experience.title}</h3>
                            <p>{experience.description}</p>
                            <p>
                                {experience.startDate.toLocaleDateString()} -{' '}
                                {experience.endDate.toLocaleDateString()}
                            </p>
                            <button style={{ marginRight: '10px' }} onClick={() => openModalForEdit(experience)}>Edit</button>
                            <button onClick={() => handleDeleteExperience(experience.id)}>Delete</button>
                        </div>
                    ))}
            </div>
        </div>
    );
};

export default Experience;