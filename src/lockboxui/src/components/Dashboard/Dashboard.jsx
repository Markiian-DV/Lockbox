/* eslint-disable react/prop-types */
import React, { useEffect, useState } from "react";
import axios from "axios";
import FileItem from "../Files/FileItem";

const Dashboard = () => {
	const [files, setFiles] = useState([]);

	useEffect(() => {
		const url = "/api/Files/list";
		axios
			.get(url)
			.then((response) => {
				setFiles(response.data);
			})
			.catch((err) => {
				console.log(err);
			});
	}, []);

	return (
		<div style={styles.wrapper} className="m-3 ">
			<h3>File managment</h3>
			<button>upload file</button>
			<div className="m-5 " style={styles.listFiles}>
				<div className="container m-0 p-1">
					{files.map((file) => (
						<FileItem file={file} key={file.fileId} />
					))}
				</div>
			</div>
		</div>
	);
};

export default Dashboard;

const styles = {
	wrapper: {
		border: "1px solid #e2e3e4",
		boxShadow: "5px 10px 18px #e2e3e4",
		minWidth: "1000px",
		maxWidth: "100rem",
	},
	listFiles: {
		backgroundColor: "var(--main-background)",
	},
};
