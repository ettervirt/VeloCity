import { z } from 'zod';

export const registerSchema = z.object({
    name: z
        .string()
        .trim()
        .min(1, { message: 'Imię jest wymagane' })
        .max(50, { message: 'Imię jest za długie' }),

    surname: z
        .string()
        .trim()
        .min(1, { message: 'Nazwisko jest wymagane' })
        .max(50, { message: 'Nazwisko jest za długie' }),

    email: z.email({
        error: (issue) =>
        issue.input === undefined
            ? 'E-mail jest wymagany'
            : 'To nie jest poprawny adres e-mail' }),

    password: z
        .string()
        .min(6, { message: "Hasło musi mieć minimum 6 znaków" })
        .max(100),

    password_confirmation: z
        .string()
        .min(1, { message: "Musisz potwierdzić hasło" }),
}).refine(
    (values) => values.password === values.password_confirmation,
    {
        message: "Hasła nie są zgodne",
        path: ['password_confirmation'],
    }
);

export type RegisterInput = z.infer<typeof registerSchema>;
