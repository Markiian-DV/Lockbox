import React, { useEffect, useState } from "react";
import Logo from "../../img/logo.svg";
import axios from "axios";

const Header = () => {
	// Mock action handlers
	const handleAnalyticsClick = () => {
		alert("Navigating to Analytics!");
		const urlApi = "/api/test";
		const api = axios
			.get(urlApi)
			.then((response) => {
				console.log(response)
			});
	};

	const handleSettingsClick = () => {
		alert("Opening Settings!");
	};

	const handleLogoutClick = () => {
		alert("Logging out!");
	};

	const [email, setEmail] = useState(null);
	const [isLoggedIn, setIsLoggedIn] = useState(false);

	useEffect(() => {
		const getInfoUrl = "/api/identity/manage/info";
		axios
			.get(getInfoUrl)
			.then((response) => {
				setEmail(response.data.email);
				setIsLoggedIn(true);
			})
			.catch(() => setIsLoggedIn(false));
	});

	return (
		<header style={styles.header}>
			<div style={styles.logoSection}>
				<img src={Logo} alt="Lockbox Logo" style={styles.logo} />
				<span style={styles.title}>Lockbox</span>
			</div>

			{isLoggedIn && (
				<div style={styles.userInfo}>
					<button style={styles.navButton} onClick={handleAnalyticsClick}>
						Analytics
					</button>
					<span style={styles.userEmail}>{email}</span>
					<button style={styles.navButton} onClick={handleSettingsClick}>
						Settings
					</button>
					<button style={styles.navButton} onClick={handleLogoutClick}>
						Logout
					</button>
				</div>
			)}
		</header>
	);
};

const styles = {
	header: {
		display: "flex",
		justifyContent: "space-between",
		alignItems: "center",
		padding: "10px 20px",
		backgroundColor: "var(white-background)",
		boxShadow: "var(--box-shadow)",
		borderBottom: "1px solid #d7e3ef",
	},
	logoSection: {
		display: "flex",
		alignItems: "center",
	},
	logo: {
		height: "40px",
		marginRight: "10px",
	},
	title: {
		fontSize: "1.5rem",
		fontWeight: "bold",
		color: "#333",
	},
	nav: {
		flexGrow: 1,
		textAlign: "center",
	},
	navButton: {
		background: "none",
		border: "none",
		fontSize: "0.9rem",
		color: "#555",
		cursor: "pointer",
		padding: "5px 10px",
	},
	userInfo: {
		display: "flex",
		alignItems: "center",
		gap: "10px",
	},
	userEmail: {
		fontSize: "0.9rem",
		color: "#555",
		padding: "5px 10px",
	},
};

export default Header;
