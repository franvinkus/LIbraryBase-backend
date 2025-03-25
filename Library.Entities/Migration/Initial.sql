CREATE DATABASE LibraryBase;
GO

USE LibraryBase;
GO

CREATE TABLE Roles (
    role_id INT IDENTITY(1,1) PRIMARY KEY,
    role_name VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE Users (
    user_id INT IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    img VARCHAR(255) NULL,
    role_id INT NOT NULL,
    created_at DATETIME DEFAULT GETDATE(), 
    created_by INT NULL, 
    updated_at DATETIME DEFAULT GETDATE(),
    updated_by INT NULL,
    FOREIGN KEY (role_id) REFERENCES Roles(role_id)
);

CREATE TABLE Categories (
    category_id INT IDENTITY(1,1) PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL UNIQUE,
    created_at DATETIME DEFAULT GETDATE(),
    created_by INT NULL,
    updated_at DATETIME DEFAULT GETDATE(),
    updated_by INT NULL
);

CREATE TABLE Books (
    book_id INT IDENTITY(1,1) PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    author VARCHAR(100) NOT NULL,
    description TEXT NULL,
    availability BIT DEFAULT 1, -- 1 = tersedia, 0 = dipinjam
    availability_date DATETIME NULL,
    img VARCHAR(255) NULL, -- Tambahkan kolom img untuk menyimpan path gambar
    created_at DATETIME DEFAULT GETDATE(),
    created_by INT NULL,
    updated_at DATETIME DEFAULT GETDATE(),
    updated_by INT NULL
);

CREATE TABLE BookCategories (
    book_id INT NOT NULL,
    category_id INT NOT NULL,
    PRIMARY KEY (book_id, category_id),
    FOREIGN KEY (book_id) REFERENCES Books(book_id) ON DELETE CASCADE,
    FOREIGN KEY (category_id) REFERENCES Categories(category_id) ON DELETE CASCADE
);

CREATE TABLE Booking (
    booking_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    book_id INT NOT NULL,
    booking_date DATETIME DEFAULT GETDATE(),
    pickup_deadline DATETIME NULL,
    return_date DATETIME NULL,
    status VARCHAR(20) CHECK (status IN ('pending', 'borrowed', 'returned')) NOT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    created_by INT NULL,
    updated_at DATETIME DEFAULT GETDATE(),
    updated_by INT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id),
    FOREIGN KEY (book_id) REFERENCES Books(book_id)
);