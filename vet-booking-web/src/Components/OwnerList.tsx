import { useState } from "react";
import type { OwnerResponse } from "../types";



export default function OwnerList() {
    const [owners, setOwners] = useState<OwnerResponse[]>([]);
    const [error, setError] = useState<string | null>(null);

    const [query, setQuery] = useState<string>("");
}