import { useState, useEffect } from "react";
import { getJson } from "../api";
import type { VetResponse } from "../types";
import ContentCard from "./ContentCard";
import SeachBar from "./Searchbar";


export default function VetList() {
    const [vets, setvets] = useState<VetResponse[]>([]);
    const [error, setError] = useState<string | null>(null);

    const [query, setQuery] = useState<string>("");

    const filterVets = query.trim() === ""
        ? vets
        : vets.filter((item) => 
            item?.name?.toLowerCase().includes(query.toLowerCase())
        );

   



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
        <div> 
            <h2> Veterinarians </h2>

             <div className="flex justify-end">

                    <SeachBar  
                        placeholder="Search Vets..."
                        value={query}
                        onChange={(e) => setQuery(e.target.value)}

                    />
                </div>

            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4 mt-10"> 
                
                


                    {filterVets.length > 0 ? (
                        filterVets.map((vet) => (
                            <ContentCard
                                key={vet.id}
                                title={vet.name}
                                subtitle={`Specialisation: ${vet.specialisation}`}
                                actions={
                                    <div>
                                    <div className="flex items-center gap-1.5">
                                        <span className="text-slate-700 font-medium">Status:</span>
                                        <span className="inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium bg-emerald-100 text-emerald-900">
                                        Active
                                        </span>
                                    </div>
                                    <div className="flex items-center">
                                        <span className="inline-flex items-center gap-2 text-slate-700 font-medium">Contact Info:  
                                        <svg
                                            xmlns="http://www.w3.org/2000/svg"
                                            fill="200"
                                            viewBox="0 0 24 24"
                                            strokeWidth={0}
                                            stroke="currentColor"
                                            className="w-5 h-5 text-slate-400 shrink-0 cursor-pointer"
                                        >
                                            <path
                                                strokeLinecap="round"
                                                strokeLinejoin="round"
                                                d="M2.25 6.75c0 8.284 6.716 15 15 15h2.25a2.25 2.25 0 0 0 2.25-2.25v-1.372c0-.516-.351-.966-.852-1.091l-4.423-1.106c-.44-.11-.902.055-1.173.417l-.97 1.293c-.282.376-.769.542-1.21.38a12.035 12.035 0 0 1-7.143-7.143c-.162-.441.004-.928.38-1.21l1.293-.97c.363-.271.527-.734.417-1.173L6.963 3.102a1.125 1.125 0 0 0-1.091-.852H4.5A2.25 2.25 0 0 0 2.25 4.5v2.25Z"
                                            />
                                        </svg>
                                        <svg
                                            xmlns="http://www.w3.org/2000/svg"
                                            fill="black"
                                            viewBox="0 0 24 24"
                                            strokeWidth={1.5}
                                            stroke="currentColor"
                                            className="w-5 h-5 text-white shrink-0 cursor-pointer"
                                        >
                                            <path
                                                strokeLinecap="round"
                                                strokeLinejoin="round"
                                                d="M21.75 6.75v10.5a2.25 2.25 0 0 1-2.25 2.25h-15a2.25 2.25 0 0 1-2.25-2.25V6.75m19.5 0A2.25 2.25 0 0 0 19.5 4.5h-15a2.25 2.25 0 0 0-2.25 2.25m19.5 0v.243a2.25 2.25 0 0 1-1.07 1.916l-7.5 4.615a2.25 2.25 0 0 1-2.36 0L3.32 8.91a2.25 2.25 0 0 1-1.07-1.916V6.75"
                                            />
                                        </svg>
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
    );
}

