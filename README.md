# AmazeCare Backend 🏥

AmazeCare Backend is a RESTful Web API built using C# and ASP.NET Core (.NET)** for a Hospital Management System.  
This backend is designed to handle the complete hospital workflow including Patient Registration, Doctor Management, Appointment Scheduling, Consultation Updates, Medical Records, Prescriptions, and Reports.

The API follows a clean structure with proper validation, exception handling, and secure authentication using JWT, making it reliable and easy to integrate with any frontend application like **React.

---

🚀 Key Features
🔐 Authentication & Security
- Secure login using JWT Authentication
- Role-based access control for:
  - Admin
  - Doctor
  - Patient
- Protected endpoints for sensitive operations

👨‍⚕️ Doctor Module
- Manage doctor profile information
- View appointments (upcoming & completed)
- Update consultation details after patient visit
- Add prescription and recommended tests

🧑‍🤝‍🧑 Patient Module
- Patient registration and profile management
- Book new appointments with doctors
- View upcoming appointments
- Cancel or reschedule appointments
- View completed consultation history

📅 Appointment Module
- Appointment booking system
- Status tracking (upcoming / completed / cancelled)
- Reschedule and cancel support
- Doctor can reject or confirm appointments

📄 Medical Records & Prescription
- Stores consultation history
- Tracks symptoms, diagnosis, treatment plan
- Supports prescription entry (medicine + dosage timing)
- Supports recommended medical tests

🗄 Database & ORM
- Uses SQL Server for database storage
- Uses Entity Framework Core for ORM and data access
- Structured entities for doctors, patients, appointments, and records

 ✅ Validations & Exception Handling
- Backend input validations for better data integrity
- User-friendly error messages
- Centralized exception handling for clean responses

---

🔗 APIs Included

- **Auth APIs** (Register, Login, JWT Token)
- **Patient APIs** (Add, Update, Delete, View)
- **Doctor APIs** (Add, Update, Delete, View)
- **Appointment APIs** (Book, View, Cancel, Reschedule)
- **Consultation APIs** (Add diagnosis, symptoms, treatment plan)
- **Medical Record APIs** (View patient history)
- **Prescription & Test APIs** (Doctor updates)

---
 🛠 Tech Stack

- Backend: ASP.NET Core Web API  
- Language: C#  
- Database: SQL Server  
- ORM: Entity Framework Core  
- Authentication: JWT (JSON Web Token)  
- API Testing: Swagger  

---

## ▶️ How to Run

### 1) Clone the repository
```bash
git clone https://github.com/madhusai85/AmazeCare-Backend.git
