export interface VetResponse {
    id: number;
    name: string;
    specialisation: string;
    email: string;
    phoneNumber: string;
}


export interface OwnerResponse {
    id: number;
    name: string;
    email: string;
    address: string;
    phoneNumber: string;
    pets: PetResponse[];
}

export interface PetResponse {
    id: number;
    name: string;
    species: string;
    breed: string;
    dateOfBirth: string;
    ownerId: OwnerResponse;
}