CREATE DATABASE IF NOT EXISTS `lab3-mysql-db`;

USE `lab3-mysql-db`;

CREATE TABLE IF NOT EXISTS `task` (
    
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `title` VARCHAR(255) NOT NULL,
    `description` VARCHAR(255),
    `created_date` DATE NOT NULL,
    `due_date` DATE,
    `status` VARCHAR(100) NOT NULL
);

INSERT INTO task (`title`, `description`, `created_date`, `due_date`, `status`) VALUES
('Implement login functionality', 'Create login form and integrate with authentication system', '2024-04-01', '2024-04-10', 'in_progress'),
('Design database schema', 'Create entity-relationship diagram and define database tables', '2024-04-02', '2024-04-15', 'pending'),
('Implement REST API endpoints', 'Develop endpoints for CRUD operations', '2024-04-03', '2024-04-12', 'completed'),
('Write unit tests', 'Create unit tests for login functionality', '2024-04-04', '2024-04-11', 'pending'),
('Refactor code for better performance', 'Optimize code for faster execution', '2024-04-05', '2024-04-14', 'in_progress'),
('Deploy application to staging server', 'Prepare application for testing by deploying to staging server', '2024-04-06', '2024-04-13', 'completed'),
('Fix bugs reported by QA team', 'Address issues identified during testing phase', '2024-04-07', '2024-04-16', 'in_progress'),
('Write documentation', 'Create user manual and developer documentation', '2024-04-08', '2024-04-17', 'pending'),
('Prepare for production deployment', 'Finalize configuration and prepare for deployment to production server', '2024-04-09', '2024-04-18', 'pending'),
('Release version 1.0', 'Publish the application for public use', '2024-04-10', '2024-04-19', 'pending'),
('Implement user profile functionality', 'Allow users to manage their profiles', '2024-04-11', '2024-04-20', 'pending'),
('Optimize database queries', 'Improve performance of database queries', '2024-04-12', '2024-04-21', 'pending'),
('Integrate third-party API', 'Incorporate external API for additional functionality', '2024-04-13', '2024-04-22', 'pending'),
('Conduct user acceptance testing', 'Have end-users test the application for usability and functionality', '2024-04-14', '2024-04-23', 'pending'),
('Prepare release notes', 'Document changes and updates for version 1.0 release', '2024-04-15', '2024-04-24', 'pending'),
('Implement error logging', 'Add logging functionality to track errors and exceptions', '2024-04-16', '2024-04-25', 'pending'),
('Update dependencies', 'Upgrade libraries and dependencies to latest versions', '2024-04-17', '2024-04-26', 'pending'),
('Perform security audit', 'Check for vulnerabilities and implement security measures', '2024-04-18', '2024-04-27', 'pending'),
('Prepare for project presentation', 'Organize materials and practice presentation for project demonstration', '2024-04-19', '2024-04-28', 'pending'),
('Submit final project deliverables', 'Upload project files and documentation for evaluation', '2024-04-20', '2024-04-29', 'pending');

