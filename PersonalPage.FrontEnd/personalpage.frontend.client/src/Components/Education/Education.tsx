// Import necessary modules and styles
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './Modal.css';

// Define the EducationModel interface to describe the shape of education objects
interface EducationModel {
    id: number;
    university: string;
    qualification: string;
    description: string;
    startDate: Date;
    endDate: Date;
}

// Main component
const Education: React.FC = () => {
    // State to hold the list of education entries
    const [educations, setEducations] = useState<EducationModel[]>([]);
    // State to hold the form data for a new or edited education entry
    const [newEducation, setNewEducation] = useState<EducationModel>({
        id: 0,
        university: '',
        qualification: '',
        description: '',
        startDate: new Date(),
        endDate: new Date(),
    });

    // State to control whether the form is in editing mode
    const [isEditing, setIsEditing] = useState(false);
    // State to hold the id of the education entry being edited
    const [editId, setEditId] = useState<number | null>(null);

    // State to control the visibility of the modal
    const [showModal, setShowModal] = useState(false);

    // Fetch education entries when the component mounts
    useEffect(() => {
        fetchEducations();
    }, []);

    // Fetch education entries from the API
    const fetchEducations = async () => {
        try {
            const response = await axios.get<EducationModel[]>('https://localhost:7158/api/Education');
            console.log('Response data:', response.data);

            // Convert startDate and endDate strings to Date objects
            const educationsWithDates = response.data.map((education) => ({
                ...education,
                startDate: new Date(education.startDate),
                endDate: new Date(education.endDate)
            }));

            setEducations(educationsWithDates || []);
        } catch (error) {
            console.error('Error fetching educations:', error);
        }
    };

    // Handle form submission for adding or updating an education entry
    const handleAddEducation = async (event: React.FormEvent) => {
        event.preventDefault();

        try {
            if (isEditing) {
                // Update existing education entry
                await axios.put(`https://localhost:7158/api/Education/${editId}`, newEducation);
            } else {
                // Add new education entry
                await axios.post('https://localhost:7158/api/Education', newEducation);
            }

            console.log('Education added/updated successfully.');

            // Reset form state after submission
            setNewEducation({
                id: 0,
                university: '',
                qualification: '',
                description: '',
                startDate: new Date(),
                endDate: new Date(),
            });

            fetchEducations();
            setShowModal(false);
            setIsEditing(false);
            setEditId(null);
        } catch (error) {
            console.error('Error adding/updating education:', error);
        }
    };

    // Handle deletion of an education entry
    const handleDeleteEducation = async (id: number) => {
        try {
            await axios.delete(`https://localhost:7158/api/Education/${id}`);
            fetchEducations();
        } catch (error) {
            console.error('Error deleting education:', error);
        }
    };

    // Handle changes in form fields
    const handleChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = event.target;

        if (name === 'startDate' || name === 'endDate') {
            setNewEducation(prevEducation => ({
                ...prevEducation,
                [name]: new Date(value),
            }));
        } else {
            setNewEducation(prevEducation => ({
                ...prevEducation,
                [name]: value,
            }));
        }
    };

    // Open modal for adding a new education entry
    const openModalForAdd = () => {
        setShowModal(true);
        setIsEditing(false);
    };

    // Open modal for editing an existing education entry
    const openModalForEdit = (education: EducationModel) => {
        setNewEducation(education);
        setShowModal(true);
        setIsEditing(true);
        setEditId(education.id);
    };

    // Close the modal
    const closeModal = () => {
        setShowModal(false);
        setIsEditing(false);
        setEditId(null);
    };

    return (
        <div>
            {/* Header and Add button */}
            <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', marginBottom: '20px' }}>
                <h1 style={{ marginRight: '10px' }}>Education</h1>
                <button style={{ marginTop: '15px' }} onClick={openModalForAdd}>Add</button>
            </div>

            {/* Modal for adding or editing education entries */}
            {showModal && (
                <div className="modal">
                    <div className="modal-content" style={{ height: '300px' }}>
                        <span className="close" onClick={closeModal}>&times;</span>
                        <h2>{isEditing ? 'Edit Education' : 'Add New Education'}</h2>
                        <form onSubmit={handleAddEducation} style={{ display: 'flex', flexDirection: 'column' }}>
                            <table>
                                <tr>
                                    <td>
                                        <label>
                                            University:
                                        </label>
                                    </td>
                                    <td>
                                        <input
                                            type="text"
                                            name="university"
                                            value={newEducation.university}
                                            onChange={handleChange}
                                            required
                                        />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Qualification:
                                        </label>
                                    </td>
                                    <td>
                                        <input
                                            type="text"
                                            name="qualification"
                                            value={newEducation.qualification}
                                            onChange={handleChange}
                                            required
                                        />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Description:
                                        </label>
                                    </td>
                                    <td>
                                        <textarea
                                            name="description"
                                            value={newEducation.description}
                                            onChange={handleChange}
                                            required
                                        />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Start Date:
                                        </label>
                                    </td>
                                    <td>
                                        <input
                                            type="date"
                                            name="startDate"
                                            value={newEducation.startDate.toISOString().split('T')[0]}
                                            onChange={handleChange}
                                            required
                                        />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            End Date:
                                        </label>
                                    </td>
                                    <td>
                                        <input
                                            type="date"
                                            name="endDate"
                                            value={newEducation.endDate.toISOString().split('T')[0]}
                                            onChange={handleChange}
                                            required
                                        />
                                    </td>
                                </tr>
                            </table>
                            <button type="submit" style={{ marginTop: '20px' }}>
                                {isEditing ? 'Update Education' : 'Add Education'}
                            </button>
                        </form>
                    </div>
                </div>
            )}

            {/* List of education entries */}
            <div>
                {educations.map((education) => (
                    <div key={education.id} style={{ marginBottom: '10px' }}>
                        <h3>{education.university}</h3>
                        <h3>{education.qualification}</h3>
                        <p>{education.description}</p>
                        <p>
                            {education.startDate.toLocaleDateString()} -{' '}
                            {education.endDate.toLocaleDateString()}
                        </p>
                        <button style={{ marginRight: '10px' }} onClick={() => openModalForEdit(education)}>Edit</button>
                        <button onClick={() => handleDeleteEducation(education.id)}>Delete</button>
                    </div>
                ))}
            </div>
        </div>
    );
};

// Export the Education component
export default Education;
