import React, { useEffect, useState } from "react";
import axios from "axios";

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
		<>
			{files.map((file) => (
				<div key={file.fileId}>
					<p>{file.fileId}</p>
					<p>{file.fileName}</p>
					<p>{file.ownerEmail}</p>
					<p>{file.accessLevel}</p>
				</div>
			))}
		</>
	);
};

export default Dashboard;
