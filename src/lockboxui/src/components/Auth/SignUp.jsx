import React, { useEffect, useState } from "react";
import emailIcon from "../../img/email.svg";
import passwordIcon from "../../img/password.svg";
import { validate } from "./validate";
import styles from "./SignUp.module.css";
import "react-toastify/dist/ReactToastify.css";
import { ToastContainer, toast } from "react-toastify";
import { notify } from "./toast";
import { Link } from "react-router-dom";
import axios from "axios";

const SignUp = () => {
  const [data, setData] = useState({
    email: "",
    password: "",
    confirmPassword: "",
  });

  const [errors, setErrors] = useState({});
  const [touched, setTouched] = useState({});

  useEffect(() => {
    setErrors(validate(data, "signUp"));
  }, [data, touched]);

  const changeHandler = (event) => {
    setData({ ...data, [event.target.name]: event.target.value });
  };

  const focusHandler = (event) => {
    setTouched({ ...touched, [event.target.name]: true });
  };

  const submitHandler = async (event) => {
    event.preventDefault();
    if (!Object.keys(errors).length) {
      const urlApi = "https://localhost:7080/api/identity/register";

      axios
        .post(urlApi, {
          email: data.email,
          password: data.password,
        })
        .then((response) => {
          console.log(response);
        })
        .catch((err) => {
          const errorsObj = err.response.data.errors;
          const errors = Object.getOwnPropertyNames(errorsObj).map(
            (k) => errorsObj[k][0]
          );
          errors.forEach((error) => {
            notify(error);
          });
        });
    } else {
      notify("Please Check fileds again", "error");
      setTouched({
        email: true,
        password: true,
        confirmPassword: true,
      });
    }
  };

  return (
    <div className={styles.container}>
      <form
        className={styles.formLogin}
        onSubmit={submitHandler}
        autoComplete="off"
      >
        <h2>Sign Up</h2>
        <div>
          <div
            className={
              errors.email && touched.email
                ? styles.unCompleted
                : !errors.email && touched.email
                ? styles.completed
                : undefined
            }
          >
            <input
              type="text"
              name="email"
              value={data.email}
              placeholder="E-mail"
              onChange={changeHandler}
              onFocus={focusHandler}
              autoComplete="off"
            />
            <img src={emailIcon} alt="" />
          </div>
          {errors.email && touched.email && (
            <span className={styles.error}>{errors.email}</span>
          )}
        </div>
        <div>
          <div
            className={
              errors.password && touched.password
                ? styles.unCompleted
                : !errors.password && touched.password
                ? styles.completed
                : undefined
            }
          >
            <input
              type="password"
              name="password"
              value={data.password}
              placeholder="Password"
              onChange={changeHandler}
              onFocus={focusHandler}
              autoComplete="off"
            />
            <img src={passwordIcon} alt="" />
          </div>
          {errors.password && touched.password && (
            <span className={styles.error}>{errors.password}</span>
          )}
        </div>
        <div>
          <div
            className={
              errors.confirmPassword && touched.confirmPassword
                ? styles.unCompleted
                : !errors.confirmPassword && touched.confirmPassword
                ? styles.completed
                : !errors.confirmPassword && touched.confirmPassword
                ? styles.completed
                : undefined
            }
          >
            <input
              type="password"
              name="confirmPassword"
              value={data.confirmPassword}
              placeholder="Confirm Password"
              onChange={changeHandler}
              onFocus={focusHandler}
              autoComplete="off"
            />
            <img src={passwordIcon} alt="" />
          </div>
          {errors.confirmPassword && touched.confirmPassword && (
            <span className={styles.error}>{errors.confirmPassword}</span>
          )}
        </div>
        <div>
          <button type="submit">Create Account</button>
          <span
            style={{
              color: "#a29494",
              textAlign: "center",
              display: "inline-block",
              width: "100%",
            }}
          >
            Already have a account? <Link to="/login">Sign In</Link>
          </span>
        </div>
      </form>
      <ToastContainer />
    </div>
  );
};

export default SignUp;
