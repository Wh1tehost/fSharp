open System
open System.IO

// Функция для получения списка файлов, начинающихся с заданного символа 
let getFiles directory firstChar =
    seq {
        for file in Directory.EnumerateFiles(directory) do // Для каждого файла из последовательности файлов (Функция возвращает последовательность всех файлов в каталоге) выполняется условие
            let fileName = Path.GetFileName(file) // Получаем имя файла из пути
            if fileName.[0] = firstChar then // Проверяем первый символ файла с заданным символом
                yield fileName  // Возвращаем только имя файла
    }

// Функция для ввода пути к каталогу
let rec inputDirectory() =
    printfn "Введите путь к каталогу:"
    let directory = Console.ReadLine()

    if Directory.Exists(directory) then // Проверка существования директории
        directory 
    else
        printfn "Каталог '%s' не существует. Попробуйте ещё раз." directory
        inputDirectory()  // Повторный запрос

// Функция для ввода символа 
let rec inputStartChar() =
    printfn "Введите символ, с которого должно начинаться имя файла:"
    let input = Console.ReadLine()

    if input.Length = 1 then // Проверка на длину строки
        input.[0]
    else
        printfn "Ошибка: введите один символ. Попробуйте ещё раз."
        inputStartChar()  // Повторный запрос

// Основная функция
let main() =
    printfn "Программа для нахождения файлов в заданном каталоге, начинающихся на заданный символ."

    // Ввод пути к каталогу
    let directory = inputDirectory()

    // Ввод символа
    let startChar = inputStartChar()

    // Получаем последовательность файлов, начинающихся с заданного символа
    let files = getFiles directory startChar

    // Вывод результата
    if Seq.isEmpty files then
        printfn "Файлов, начинающихся с символа '%c', в каталоге '%s' не найдено." startChar directory
    else
        printfn "Файлы, начинающиеся с символа '%c':" startChar
        files 
        |> Seq.iter (printfn "%s")  // Для каждого элемента последовательности производится вывод в отдельной строке
main()


///Users/vladverholancev/Documents/Test