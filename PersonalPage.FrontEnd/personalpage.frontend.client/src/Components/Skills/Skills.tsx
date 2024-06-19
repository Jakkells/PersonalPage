// Import necessary modules and styles
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './Modal.css';

// Define the SkillsModel interface to describe the shape of skill objects
interface SkillsModel {
    id: number;
    skill: string;
    qualification: string;
    description: string;
}

// Main component
const Skills: React.FC = () => {
    // State to hold the list of skills
    const [skills, setSkills] = useState<SkillsModel[]>([]);
    // State to hold the form data for a new or edited skill
    const [newSkill, setNewSkill] = useState<SkillsModel>({
        id: 0,
        skill: '',
        qualification: '',
        description: '',
    });

    // State to track if the form is in editing mode
    const [isEditing, setIsEditing] = useState(false);
    // State to hold the id of the skill being edited
    const [editId, setEditId] = useState<number | null>(null);

    // State for controlling modal visibility
    const [showModal, setShowModal] = useState(false);

    // Fetch skills when the component mounts
    useEffect(() => {
        fetchSkills();
    }, []);

    // Fetch skills from the API
    const fetchSkills = async () => {
        try {
            const response = await axios.get<SkillsModel[]>('https://localhost:7158/api/Skills');
            setSkills(response.data || []);
        } catch (error) {
            console.error('Error fetching skills:', error);
        }
    };

    // Handle form submission for adding or updating a skill
    const handleAddOrUpdateSkill = async (event: React.FormEvent) => {
        event.preventDefault(); // Prevent default form submission

        try {
            if (isEditing) {
                // Update existing skill
                await axios.put(`https://localhost:7158/api/Skills/${editId}`, newSkill);
            } else {
                // Add new skill
                await axios.post('https://localhost:7158/api/Skills', newSkill);
            }

            // Reset form state after submission
            setNewSkill({
                id: 0,
                skill: '',
                qualification: '',
                description: '',
            });

            fetchSkills(); // Refresh the list after adding or updating
            setShowModal(false); // Close the modal after successful addition or update
            setIsEditing(false); // Reset edit mode
            setEditId(null); // Clear edit id
        } catch (error) {
            console.error('Error adding/updating skill:', error);
        }
    };

    // Handle deletion of a skill
    const handleDeleteSkill = async (id: number) => {
        try {
            await axios.delete(`https://localhost:7158/api/Skills/${id}`);
            fetchSkills(); // Refresh the list after deletion
        } catch (error) {
            console.error('Error deleting skill:', error);
        }
    };

    // Handle changes in form fields
    const handleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = event.target;
        setNewSkill(prevSkill => ({
            ...prevSkill,
            [name]: value,
        }));
    };

    // Open modal for adding a new skill
    const openModalForAdd = () => {
        setShowModal(true);
        setIsEditing(false); // Ensure not in edit mode when adding
    };

    // Open modal for editing an existing skill
    const openModalForEdit = (skill: SkillsModel) => {
        setNewSkill(skill); // Populate form with selected skill
        setShowModal(true);
        setIsEditing(true); // Set edit mode
        setEditId(skill.id); // Set the id of the skill being edited
    };

    // Close the modal
    const closeModal = () => {
        setShowModal(false);
        setIsEditing(false); // Reset edit mode on modal close
        setEditId(null); // Clear edit id on modal close
    };

    return (
        <div style={{ marginBottom: '50px' }}>
            {/* Header and Add button */}
            <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', marginBottom: '20px' }}>
                <h1 style={{ marginRight: '10px' }}>Skills</h1>
                <button style={{ marginTop: '15px' }} onClick={openModalForAdd}>Add</button>
            </div>

            {/* Modal for adding or editing skills */}
            {showModal && (
                <div className="modal">
                    <div className="modal-content" style={{ height: '250px' }}>
                        <span className="close" onClick={closeModal}>&times;</span>
                        <h2>{isEditing ? 'Edit Skill' : 'Add New Skill'}</h2>
                        <form onSubmit={handleAddOrUpdateSkill} style={{ display: 'flex', flexDirection: 'column' }}>
                            <table>
                                <tr>
                                    <td>
                                        <label style={{ marginBottom: '10px' }}>
                                            Skill:
                                        </label>
                                    </td>
                                    <td>
                                        <input
                                            type="text"
                                            name="skill"
                                            value={newSkill.skill}
                                            onChange={handleChange}
                                            required
                                        />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label style={{ marginBottom: '10px' }}>
                                            Qualification:
                                        </label>
                                    </td>
                                    <td>
                                        <input
                                            type="text"
                                            name="qualification"
                                            value={newSkill.qualification}
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
                                            value={newSkill.description}
                                            onChange={handleChange}
                                            required
                                        />
                                    </td>
                                </tr>
                            </table>
                            <button type="submit" style={{ marginTop: '10px', width: '100%' }}>
                                {isEditing ? 'Update Skill' : 'Add Skill'}
                            </button>
                        </form>
                    </div>
                </div>
            )}

            {/* List of skills */}
            <div>
                {Array.isArray(skills) &&
                    skills.map((skill) => (
                        <div key={skill.id} style={{ marginBottom: '10px' }}>
                            <h3>{skill.skill}</h3>
                            <p>{skill.qualification}</p>
                            <p>{skill.description}</p>
                            <button style={{ marginRight: '10px' }} onClick={() => openModalForEdit(skill)}>Edit</button>
                            <button onClick={() => handleDeleteSkill(skill.id)}>Delete</button>
                        </div>
                    ))}
            </div>
        </div>
    );
};

// Export the Skills component
export default Skills;
