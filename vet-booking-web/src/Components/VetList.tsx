import { useState, useEffect } from "react";
import { getJson } from "../api";
import type { VetResponse } from "../types";

export default function VetList() {
    const [vets, setvets] = useState<VetResponse[]>([]);
    const [error, setError] = useState<string | null>(null);


    useEffect(() => {
        async function fetchVets() {
            try {
                const data = await getJson<VetResponse[]>("/Vet");
                setvets(data);
            }
            catch (e: any) {
                setError(e.message || "Failed to load Vets.");
            }
        }

        fetchVets();
    }, []);


    if (error) return <div>Error: {error}</div> 
    
    return (
        <div > 
            <h2> Veterinarians </h2>
            <ul> 
                {vets.map((vet) => (
                    <li key={vet.id}>{vet.name}</li> 
                ))} 
            </ul>
        </div>
    );
}

