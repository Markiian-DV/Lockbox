import SignUp from "./components/Auth/SignUp";
import Login from "./components/Auth/Login";
import Header from "./components/Header/Header"
import { Route, Routes, BrowserRouter } from "react-router-dom";

function App() {
  return (
    <>
      <Header></Header>
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