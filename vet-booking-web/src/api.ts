const baseUrl = import.meta.env.VITE_API_BASE_URL;

export async function getJson<T>(path: string): Promise<T> {
  const response = await fetch(`${baseUrl}${path}`);
  if (!response.ok) {
    throw new Error(`Request failed: ${response.status}`);
  }
  return response.json() as Promise<T>;
}

export async function sendJson<T>(
  path: string,
  method: "POST" | "PUT" | "DELETE",
  body?: unknown
): Promise<T | null> {
  const response = await fetch(`${baseUrl}${path}`, {
    method,
    headers: { "Content-Type": "application/json" },
    body: body ? JSON.stringify(body) : undefined,
  });
  if (!response.ok) {
    throw new Error(await response.text());
  }
  return response.status === 204 ? null : ((await response.json()) as T);
}