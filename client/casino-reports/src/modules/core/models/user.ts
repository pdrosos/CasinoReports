export class User {
  public readonly name: string;
  public readonly username: string;
  public readonly email: string;
  public readonly roles: string[];

  public constructor(name: string, username: string, email: string, roles: string[]) {
    this.name = name;
    this.username = username;
    this.email = email;
    this.roles = roles;
  }
}
