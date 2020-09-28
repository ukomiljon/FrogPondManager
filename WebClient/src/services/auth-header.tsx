export default function authHeader() {
  const account = JSON.parse(localStorage.getItem("account")!);

  if (account && account.jwtToken) {
    return { Authorization: account.jwtToken };
  } else {
    return {};
  }
}

export const originHeader = { origin: "http://localhost:5000" };
