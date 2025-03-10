open System

// Рекурсивная функция для нахождения суммы цифр числа
let rec SumDigit number currentSum =
    if number = 0 then
        currentSum
    else
        let newSum =  currentSum + number % 10    
        SumDigit (number / 10) newSum  

// Функция для выбора способа заполнения последовательности
let rec inputNumb () = 
    printfn "Введите каким способом заполнить последовательность."
    printfn "1. Ручной ввод"
    printfn "2. Случайная генерация чисел с заданным кол-вом"
    let input = System.Console.ReadLine()
    match input with 
    | "1" ->
        Manual Seq.empty  // Запуск ручного ввода (отправляется пустая последовательность)
    | "2" ->
        Auto ()  // Запуск автоматической генерации
    | _ -> 
        printfn "Некоректный ввод. Попробуйте еще раз"
        inputNumb ()  // Повторный запрос

// Функция для создания последовательности автоматическим вводом
and Auto () =
    let random = Random()
    printf "Введите кол-во чисел в последовательности: "
    let N = System.Console.ReadLine()
    printf "Введите диапазон значений в последовательности: от 1 до "
    let value = System.Console.ReadLine()
    
    match System.Int32.TryParse(N), System.Int32.TryParse(value) with
    | (true, n), (true, maxValue) when n > 0 && maxValue > 0 -> // Проверка на натуральность
        let randSeq = [ for _ in 1 .. n -> random.Next(1, maxValue + 1) ]  // Генерация списка из n чисел со случайными числами в заданном диапазоне
        randSeq |> Seq.ofList  // Преобразуем список в последовательность
    | _ ->
        printfn "Ошибка ввода. Пожалуйста, введите корректные числа."
        Auto ()  // Повторный запрос

// Функция для создания последовательности ручным вводом
and Manual acc =
    printf "Введите натуральное число или 'q' для выхода: "
    let input = System.Console.ReadLine()
    
    match input with
    | "q" -> 
        printfn "" 
        acc  // Возвращаем накопленную последовательность
    | _ ->
        match System.Int32.TryParse input with
        | true, number when number > 0 ->  // Проверка на натуральность
            // Создание новой последовательности из нового элемента и накопленной ранее последовательности
            let newAcc = seq {
                yield! acc // Добавление накопленной последовательности
                yield number
            }
            Manual newAcc  // Продолжаем ввод
        | _ ->
            printfn "Ошибка ввода. Пожалуйста, введите корректное натуральное число."
            printfn "" 
            Manual acc  // Продолжаем ввод

// Основная функция 
let main() =
    printfn "Программа для формирования последовательности сумм цифр введенных чисел."
    
    let inputSeq = inputNumb ()  // Выбор способа заполнения последовательности
    printfn "Исходная последовательность: %A" (inputSeq |> Seq.toList) // Вывод последдовательности путем преобразования в список
    
    let newSeq = Seq.map (fun a -> SumDigit a 0) inputSeq  // Для каждого элемента a выполняется функция 
    printfn "Последовательность сумм цифр: %A" (newSeq |> Seq.toList) // Вывод последдовательности путем преобразования в список
main()