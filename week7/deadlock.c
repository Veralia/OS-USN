#include <stdio.h>
#include <pthread.h>
#include <unistd.h>

// Two mutexes (locks) that will cause deadlock
pthread_mutex_t lock1 = PTHREAD_MUTEX_INITIALIZER;
pthread_mutex_t lock2 = PTHREAD_MUTEX_INITIALIZER;

// Thread 1: Acquires lock1 first, then lock2
void* thread1_function(void* arg) {
    printf("Thread 1: Attempting to acquire lock1...\n");
    pthread_mutex_lock(&lock1);
    printf("Thread 1: Acquired lock1\n");
    
    // Small delay to ensure thread 2 gets lock2
    sleep(1);
    
    printf("Thread 1: Attempting to acquire lock2...\n");
    pthread_mutex_lock(&lock2);  // This will block, waiting for lock2
    printf("Thread 1: Acquired lock2\n");
    
    // Critical section
    printf("Thread 1: In critical section with both locks\n");
    
    pthread_mutex_unlock(&lock2);
    pthread_mutex_unlock(&lock1);
    printf("Thread 1: Released both locks\n");
    
    return NULL;
}

// Thread 2: Acquires lock2 first, then lock1 (OPPOSITE ORDER - causes deadlock)
void* thread2_function(void* arg) {
    printf("Thread 2: Attempting to acquire lock2...\n");
    pthread_mutex_lock(&lock2);
    printf("Thread 2: Acquired lock2\n");
    
    // Small delay to ensure thread 1 gets lock1
    sleep(1);
    
    printf("Thread 2: Attempting to acquire lock1...\n");
    pthread_mutex_lock(&lock1);  // This will block, waiting for lock1
    printf("Thread 2: Acquired lock1\n");
    
    // Critical section
    printf("Thread 2: In critical section with both locks\n");
    
    pthread_mutex_unlock(&lock1);
    pthread_mutex_unlock(&lock2);
    printf("Thread 2: Released both locks\n");
    
    return NULL;
}

int main() {
    pthread_t thread1, thread2;
    
    printf("=== Deadlock Demonstration ===\n");
    printf("Creating two threads that will deadlock...\n\n");
    
    // Create both threads
    pthread_create(&thread1, NULL, thread1_function, NULL);
    pthread_create(&thread2, NULL, thread2_function, NULL);
    
    // Wait for threads (this will hang due to deadlock)
    pthread_join(thread1, NULL);
    pthread_join(thread2, NULL);
    
    printf("Program completed successfully (this won't print due to deadlock)\n");
    
    return 0;
}

/*
 * EXPLANATION:
 * 
 * This program demonstrates a classic deadlock scenario:
 * 
 * 1. Thread 1 acquires lock1, then tries to acquire lock2
 * 2. Thread 2 acquires lock2, then tries to acquire lock1
 * 
 * DEADLOCK OCCURS:
 * - Thread 1 holds lock1 and waits for lock2
 * - Thread 2 holds lock2 and waits for lock1
 * - Neither can proceed (circular wait condition)
 * 
 * All four conditions for deadlock are met:
 * 1. Mutual exclusion - locks can only be held by one thread
 * 2. Hold and wait - threads hold one lock while waiting for another
 * 3. No preemption - locks cannot be forcibly taken away
 * 4. Circular wait - Thread 1 waits for Thread 2, Thread 2 waits for Thread 1

 */
