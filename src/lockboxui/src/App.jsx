import SignUp from "./components/Auth/SignUp";
import Login from "./components/Auth/Login";
import Header from "./components/Header/Header";
import Dashboard from "./components/Dashboard/Dashboard";
import { Route, Routes, BrowserRouter } from "react-router-dom";
import withAuth from "./components/hoc/withAuth";

function App() {
	return (
		<>
			<Header></Header>
			<BrowserRouter>
				<Routes>
					<Route exact path="/login" element={<Login />} />
					<Route exact path="/signup" element={<SignUp />} />
					<Route exact path="/dashboard" Component={withAuth(Dashboard)} />
					<Route path="*" Component={withAuth(Dashboard)} />
				</Routes>
			</BrowserRouter>
		</>
	);
}

export default App;
