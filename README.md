# ⚖️ Quantity Measurement App

## 📝 Project Overview

The **Quantity Measurement App** is a simple application that compares different quantities such as **length** and **weight**.  
Its main goal is to ensure **accurate comparison** between values that may belong to the same or different units.

The application is being developed **step by step**, keeping each use case small and focused.

## 🚀 Application Progression

The project grows in stages:

### 1. Comparison
- Compare two quantities of the same unit (for example, feet to feet).
- Return whether both values are equal or not.

### 2. Conversion
- Convert a value from one unit to another (for example, feet to inches).

### 3. Arithmetic
- Perform arithmetic operations like addition or subtraction on quantities.

## 🎯 Design Approach

- Each **Use Case (UC)** is implemented in a separate branch.
- No extra features are added beyond the given requirements.
- Clean structure with **Model**, **Business**, and **Test** layers.
- Unit tests are written to ensure correctness at every step.

---

## 🚀 Use Case 1: Feet Measurement Equality

### Objective
Compare two **Feet** objects based on their values, not their memory references.

### Implementation
Value comparison is implemented by overriding `Equals()` and `GetHashCode()` in the `Feet` model class.

### Key Learning
Learned the difference between **reference equality** and **value equality** in C#.

### Branch
`feature/UC1-FeetMeasurementEquality`
