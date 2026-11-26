interface AppointmentModalProps {
    item: any;
    onClose: () => void;
    onViewDetails: (id: string) => void;
}

export default function AppointmentModal({ item, onClose, onViewDetails }: AppointmentModalProps) {
    const props = item.extendedProps;

    return (
        /* Modal Overlay */
        <div className="fixed inset-0 w-full h-full bg-black/80 flex justify-center items-center z-50 p-4 overflow-hidden">

            {/* Modal Box */}
            <div className="bg-white text-black flex flex-col justify-start items-center rounded-lg p-6 md:p-10 w-full max-w-lg max-h-[90vh] relative overflow-y-auto shadow-2xl">
                
                {/* Back Arrow / Close Button Wrapper */}
                <div className="w-full flex justify-start mb-4 shrink-0">
                    <button
                        onClick={onClose}
                        className="cursor-pointer p-1  dark:hover:bg-slate-300 rounded-full transition"
                    >
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            viewBox="0 0 24 24"
                            fill="none"
                            stroke="currentColor"
                            strokeWidth={3}
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            className="w-8 h-8"
                        >
                            <path d="M19 12H5" />
                            <path d="M12 19l-7-7 7-7" />
                        </svg>
                    </button>
                </div>

                {/* Modal Content */}
                <div className="w-full flex flex-col items-center">
                    <h2 className="text-2xl font-bold mb-1 text-center">{item.title}</h2>
                    <p className="text-gray-800  text-sm mb-4 text-center">
                        {item.start?.toLocaleString()}
                    </p>
                    
                    <hr className="border-gray-200 dark:border-gray-700 w-full mb-6"/>

                    <div className="w-full space-y-3 text-left mb-8">
                        <p><strong>Pet:</strong> {props.petName}</p>
                        <p><strong>Owner:</strong> {props.owner}</p>
                        <p><strong>Vet:</strong> Dr. {props.vetName}</p>
                        <p><strong>Exam Room:</strong> {props.room}</p>
                        <p><strong>Price:</strong> £{props.price}</p>
                        <p><strong>Status:</strong> <span>{props.status}</span></p>
                    </div>

                    {/* Action Buttons */}
                    <div className="w-full flex gap-3">
                        <button
                            onClick={() => onViewDetails(props.id)}
                            className="flex-1 bg-slate-200 text-white dark:bg-slate-700 hover:bg-slate-300 dark:hover:bg-slate-600 font-semibold py-2 px-4 rounded-lg transition"
                        >
                            Full Details
                        </button>
                        <button
                            onClick={onClose}
                            className="flex-1 bg-[#625b71] hover:bg-[#4c4854] text-white font-semibold py-2 px-4 rounded-lg transition"
                        >
                            Close
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
}