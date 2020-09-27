export default function authHeader() {
  const account = JSON.parse(localStorage.getItem("account")!);

  if (account && account.accessToken) {
    return { Authorization: account.accessToken };
  } else {
    return {};
  }
}

export const originHeader = { origin: "http://localhost:5000" };
