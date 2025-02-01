/* eslint-disable react/prop-types */

const FileItem = ({ file }) => {
	return (
		<div className="row m-2 align-items-center" style={styles.file}>
			<div className="col-md-2 text-left">{file.fileName}</div>
			<div className="col-md-1">{file.fileSize} mb</div>
			<div className="col-md-3">owner: {file.ownerEmail}</div>
			<div className="col-md-2">access: {file.accessLevel}</div>
			<div className="col-sm-2 btn-group">
				<button type="button p-1" className="btn btn-primary btn-sm w-100">
					Grant to
				</button>
				{/* </div>
			<div className="col-sm-1"> */}
				<button type="button p-1" className="btn btn-danger btn-sm w-100">
					Revoke from
				</button>
			</div>

			<div className="col-sm-1 text-right">buttons</div>
		</div>
	);
};

export default FileItem;

const styles = {
	file: {
		backgroundColor: "var(--white-background)",
		height: "60px",
	},
};
