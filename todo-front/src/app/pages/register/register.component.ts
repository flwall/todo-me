import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/services/auth.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-register",
  templateUrl: "./register.component.html",
  styleUrls: ["./register.component.scss"],
})
export class RegisterComponent implements OnInit {
  constructor(private Auth: AuthService, private router: Router) {}

  ngOnInit(): void {}

  async registerUser(event) {
    event.preventDefault();
    const target = event.target;
    const user = target.querySelector("#user").value;
    const password = target.querySelector("#password").value;
    const email = target.querySelector("#email").value;
    this.Auth.registerUser({
      username: user,
      password: password,
      email: email,
    }).subscribe(
      (data) => {
        if (data.status === 200) {
          console.log("funkt");
          this.router.navigate([""]);
        }
      },
      (error) => {
        window.alert("invalid credentials"); //refine this
        console.log(error);
      }
    );
    console.log("Input from user:" + user, password);
  }
}
