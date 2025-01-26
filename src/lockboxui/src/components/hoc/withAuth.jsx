import axios from "axios";
import { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";

const withAuth = (WrappedComponent) => {
	const AuthenticatedComponent = (props) => {
		const [isAuthenticated, setIsAuthenticated] = useState(false);
		const [loading, setLoading] = useState(true);
		useEffect(() => {
			const checkAuthentication = async () => {
				try {
					const response = await axios.get("/api/identity/manage/info");
					if (response.status === 200 && response.data.email) {
						setIsAuthenticated(true);
					}
				} catch (err) {
					setIsAuthenticated(false);
				} finally {
					setLoading(false);
				}
			};
			checkAuthentication();
		});

		if (loading) {
			return <div>Loading...</div>;
		}

		if (!isAuthenticated) {
			return <Navigate replace to="/login" />;
		}

		return <WrappedComponent {...props} />;
	};

	return AuthenticatedComponent;
};

export default withAuth;
