# 🚌 Smart Bus Seat Upgrade & Downgrade Management System

A real-world ASP.NET Core MVC application that enables passengers to upgrade or downgrade their booked bus seats after ticket confirmation with automated fare recalculation, refund handling, transaction tracking, and dynamic seat availability management.

Inspired by real-world ticketing workflows used in modern bus booking platforms.

---

# 📌 Project Overview

Traditional bus booking systems primarily focus on initial ticket booking workflows.  
This project extends the booking lifecycle by introducing a **post-booking seat management system** where passengers can:

- Verify existing bookings
- View live seat availability
- Upgrade seats
- Downgrade seats
- View fare differences
- Simulate payment/refund workflow
- Automatically update seat inventory
- Track seat modification history

---

# 🚀 Features

## ✅ Booking Verification
Passengers verify bookings using:
- Booking ID
- Phone Number

---

## ✅ Real-Time Bus Seat Blueprint UI
Interactive seat layout similar to modern bus booking applications.

Includes:
- Current Seat Highlight
- Booked Seats
- Available Seats
- Seat Pricing
- Bus Aisle Layout

---

## ✅ Seat Upgrade Workflow
Passengers can:
- Select premium seats
- Pay additional fare difference
- Confirm payment before seat update

---

## ✅ Seat Downgrade Workflow
Passengers can:
- Move to lower fare seats
- View refund amount
- Simulate refund processing

---

## ✅ Dynamic Fare Recalculation
System automatically calculates:
- Upgrade amount
- Refund amount
- Current active fare

---

## ✅ Transaction Tracking
Every seat modification is stored with:
- Previous Seat
- New Seat
- Previous Fare
- New Fare
- Difference Amount
- Action Type

---

## ✅ Database Synchronization
Seat inventory updates automatically:
- Old seat becomes available
- New seat becomes booked

---

# 🛠️ Tech Stack

| Technology | Usage |
|---|---|
| ASP.NET Core MVC | Web Application Framework |
| Entity Framework Core | ORM |
| SQLite / SQL Server | Database |
| Bootstrap 5 | Responsive UI |
| C# | Backend Development |
| Razor Views | Frontend Rendering |

---

# 🧠 Key Concepts Implemented

- MVC Architecture
- Entity Relationships
- Database Transactions
- Dynamic Pricing Logic
- Seat Inventory Management
- Payment Workflow Simulation
- Refund Calculation
- CRUD Operations
- Responsive UI Design

---

# 🗂️ Project Structure

```text
SmartBusSeatUpgradeSystem/
│
├── Controllers/
├── Models/
├── Data/
├── ViewModels/
├── Views/
├── wwwroot/
├── screenshots/
├── Program.cs
├── appsettings.json
└── README.md


# 📷 Application Screenshots

The following screenshots demonstrate the core workflow and user experience of the Smart Bus Seat Upgrade & Downgrade Management System.

---

## 🔐 Booking Verification

Passengers can securely verify their booking using Booking ID and registered phone number before accessing seat modification features.

![Verify Booking](screenshots/verify-page.png)

---

## 🚌 Interactive Bus Seat Layout

Real-time bus seat blueprint interface displaying:
- Available Seats
- Booked Seats
- Current Selected Seat
- Seat Pricing
- Upgrade/Downgrade Options

![Seat Layout](screenshots/seat-layout.png)

---

## 💳 Seat Upgrade / Downgrade Payment Summary

Dynamic fare recalculation page showing:
- Existing Seat Details
- New Seat Details
- Fare Difference
- Upgrade Charges / Refund Amount
- Payment Confirmation Workflow

![Payment Page](screenshots/payment-page.png)

---


Successful seat modification workflow with automated database synchronization and transaction update confirmation.

