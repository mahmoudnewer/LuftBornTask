
# LuftBornTask - Full Stack Angular & ASP.NET Core Project

This project is a full-stack task developed using:

- **Frontend:** Angular (Standalone Components)
- **Backend:** ASP.NET Core Web API
- **Authentication:** Auth0 with JWT

---

## 🔧 Project Structure

```
LuftBornTask-FE/           --> Angular App (Frontend)
LuftBornTask-BE/          --> ASP.NET Core Web API (Backend)
```

---

## 🚀 Getting Started

### 🖥️ Backend (ASP.NET Core Web API)

#### 📍 Prerequisites:
- [.NET SDK 8.0+]

#### 📂 Path:
```
/LuftBornTask-BE
```

#### 🔧 Steps:

1. Open the solution in Visual Studio or VS Code.
2. Ensure the API is configured to run on **https://localhost:7188**.
3. Run the API:
   ```bash
   dotnet run
   ```

---

### 🌐 Frontend (Angular Standalone App)

#### 📍 Prerequisites:
- [Node.js 18+](https://nodejs.org/)
- Angular CLI:
  ```bash
  npm install -g @angular/cli
  ```

#### 📂 Path:
```
/LuftBornTask-FE
```

#### 🔧 Steps:

1. Navigate to the project directory:
   ```bash
   cd LuftBornTask-FE
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Run Angular in **HTTPS mode on port 4200**:
   ```bash
   ng serve --ssl true --port 4200
   ```

---

## 🔐 Authentication

The app uses **Auth0** for secure login.

- You must configure your own:
  - **domain**
  - **clientId**
  - **audience**

Check this file for configuration:
```
src/app/app.config.ts
```

---

## 🔁 API Endpoints

| Method | Endpoint                     | Description                  |
|--------|------------------------------|------------------------------|
| GET    | `/api/Product/GetAll`        | Get all products             |
| GET    | `/api/Product/GetAllPaged`   | Get all products and filterd |
| GET    | `/api/Product/{id}`          | Get product by ID            |
| POST   | `/api/Product`               | Create product               |
| PUT    | `/api/Product/{id}`          | Update product               |
| DELETE | `/api/Product/{id}`          | Delete product               |

---

## 📦 API Response Format

All API responses follow this format:

```json
{
  "success": true,
  "data": ...,
  "errors": null,
  "statusCode": 200
}
```

## 💡 Notes

- Ensure both frontend and backend run on HTTPS and on Microsot edge to avoid any configuration issues.
- CORS is enabled to allow Angular frontend to call the backend.
- Protect routes using `AuthGuard` on the frontend and `[Authorize]` on the backend.


