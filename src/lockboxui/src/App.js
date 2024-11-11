import SignUp from "./components/Auth/SignUp";
import Login from "./components/Auth/Login";
import { Route, Router, Routes, Navigate, BrowserRouter } from "react-router-dom";

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route exact path="/login" element={<Login />} />
          <Route exact path="/signup" element={<SignUp />} />
          <Route path='*' element={<Login />} />
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;