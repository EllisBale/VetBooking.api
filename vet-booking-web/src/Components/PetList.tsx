import { useEffect, useState } from "react";
import { getJson } from "../api";
import type { PetResponse } from "../types";
import SeachBar from "./Searchbar";
import ContentCard from "./ContentCard";

export default function PetList() {
    const [pets, setPets] = useState<PetResponse[]>([]);
    const [error, setError] = useState<string | null>(null);

    const [query, setQuery] = useState<string>("");

    const searchLower = query.toLowerCase().trim();

    const filterPets = query.trim() === ""
        ? pets
        : pets.filter((item) => 
            item?.name?.toLowerCase().includes(searchLower) ||
            item?.species?.toLowerCase().includes(searchLower) ||
            item?.breed?.toLowerCase().includes(searchLower)
        );

    useEffect(() => {
        async function fetchPets() {
            try {
                const data = await getJson<PetResponse[]>("/Pet");
                setPets(data);
            }
            catch (e: any) {
                setError(e.message || "Failed to load Pets.");
            }
        }
        fetchPets();
    }, []);

    if (error) return <div>Error: {error}</div> 


    return (
            <div>
                <div className="flex row justify-between mb-10">
                    <h2> Pets </h2>
                    <button className="bg-teal-600 hover:bg-teal-700 text-white font-bold py-2 px-6 rounded cursor-pointer">
                       + Add New Pet
                    </button>
                </div>
    
                 <div className="flex justify-end">
                        <SeachBar  
                            placeholder="Search Pets..."
                            value={query}
                            onChange={(e) => setQuery(e.target.value)}
                        />
                    </div>
    
                <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4 mt-5"> 
    
                        {filterPets.length > 0 ? (
                            filterPets.map((pet) => (
                                <ContentCard
                                    key={pet.id}
                                    title={pet.name}
                                    subtitle={`DOB: ${pet.dateOfBirth}`}
                                    actions={
                                        <div>
                        
                                        <div className="flex items-center">
                                            <span className="inline-flex items-center gap-2 text-slate-700 font-medium">Species: {pet.species}
                                            
                                            </span>
                                        </div>
                                         <div className="flex items-center">
                                            <span className="inline-flex items-center gap-2 text-slate-700 font-medium">Owner: Placeholder
                                            
                                            </span>
                                        </div>
                                    </div>
                                    }
                                    children={
                                        <div className="flex justify-center gap-5">
                                            <button className="bg-slate-100 hover:bg-slate-200 text-slate-700 font-bold py-2 px-6 rounded cursor-pointer">
                                                Edit
                                            </button>
                                            <button className="bg-teal-600 hover:bg-teal-700 text-white font-bold py-2 px-6 rounded cursor-pointer">
                                                View Profile
                                            </button>
                                        </div>
                                    }
                                />
                            ))
    
                        ) : (
                            <div className="col-span-full text-center py-10 text-slate-500">
                                Nothing Found
                            </div>
        
                        )}
                </div>
            </div>

                    )
}