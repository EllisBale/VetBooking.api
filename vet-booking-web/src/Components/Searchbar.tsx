
interface SearchBarProps {
    placeholder?: string;
    value?: string,
    onChange?: (e: React.ChangeEvent<HTMLInputElement>) => void;
}


export default function SeachBar({
    placeholder = "Search...",
    value,
    onChange} : SearchBarProps) 
{

    return (
        <div>
            <div className="bg-white border rounded">
                <input 
                    type="text"
                    maxLength={70}
                    placeholder={placeholder}
                    value={value}
                    onChange={onChange}
                    className="p-1 px-2 outline-none"
                    id="searchbar"
                />
            </div>
        </div>
    )

}