# GradePortal - Student Grades Management System

## Overview

This project is a learning-oriented API developed to manage student grades and teacher interactions. It simulates a system where teachers can manage student information and grades, while students can securely view their grades and feedback.

The project is designed to demonstrate modular architecture, clean coding practices, and essential programming concepts in a practical application.

## Features

### Teacher's Capabilities:
1. **Student Management:**
   - Add, remove, edit, and view student details.
   - Display all students with their grades.
   - View a specific student’s grades.

2. **Grade Management:**
   - Assign grades based on exercise code, ID number, and grades.
   - Update specific student grades.
   - View grades for a specific exercise.
   - Calculate the final grade based on defined parameters (from AppSettings).
   - View all exercises and their corresponding grades.

### Student's Capabilities:
1. **Grade Viewing:**
   - View the last submitted grade using ID and password.
   - View grades for specific exercises using exercise code.
   - View exam grade.
   - View final grade.

## System Design

### Class Library
- **Grade**: Contains the following attributes:
  - Exercise Code (1, 2, 3, and Exam Code 99)
  - Exercise/Exam Name
  - Date
  - Grade
  - Feedback

- **Student**: Contains the following attributes:
  - Name
  - ID Number
  - Password (Initial set to ID number)
  - List of Grades (Object of type `Grade`)
  - Final Exam Grade (Object of type `Grade`)
  - A function that returns the average of the exercise grades.

### Data Source (Students)
- A class containing a list of students and functions to initialize three students, each with three exercise grades and an exam grade.
- Functions for adding, deleting, editing, and displaying student data.

### Controller:
- CRUD operations for student management (Create, Read, Update, Delete).
- CRUD operations for grade management.

### Services:
- Service to calculate the final grade based on the weights defined in AppSettings.
- Service to calculate the average grade for a specific exercise.
- Functionality for managing the teacher’s credentials for logging in.

### AppSettings:
- The weights for each exercise and the exam percentage.
- Teacher's username and password.

### Exception Handling:
- **StudentNotExist**: Thrown when trying to modify, delete, or view a student who does not exist.
- **StudentAlreadyExist**: Thrown when trying to create a new student that already exists.

### Logging:
- Log every action in the system, such as when a student logs in and requests a grade, or when a teacher adds a student or grade.
- Logs should contain details such as the student's ID, exercise code, and grade.
- All errors and exceptions should be logged as well.

## Technologies Used
- ASP.NET Core for building the API
- Dependency Injection for managing services
- Entity Framework (if used) for database management
- Swagger for API documentation
- Postman for API testing
- Class Library for business logic separation

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/MiriRadunsky/GradePortal.git
