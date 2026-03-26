#\!/bin/bash
# Script de backup de la lista de alumnos
# Copia el archivo de la lista de alumnos a un directorio de backup
MYSQl_CONTAIMER="mysql_work"
DB_NAME="daniel"
DB_ROOT_PASSWORD="Daniel123*"
LOCAL_BACKUP_DIR="./backups"

TIMESTAMP=$(date +"%Y%m%d_%H%M%S")
BACKUP_FILE="backup_${DB_NAME}_${TIMESTAMP}.sql"

echo "=== Respaldo de Base de Datos ==="
echo "Fecha: $(date '+%Y-%m-%d %H:%M:%S')"
echo ""

mkdir -p "$LOCAL_BACKUP_DIR"

echo "[1/1] Creando respaldo desde el contenedor '$MYSQl_CONTAIMER'..."
mysqldump -h 192.168.64.1 -P 3308 -u root -p$DB_ROOT_PASSWORD $DB_NAME > "$LOCAL_BACKUP_DIR/$BACKUP_FILE"

if [ $? -eq 0 ]; then
	echo "Error: Fallo la creacion del respaldo"
	rm -f "$LOCAL_BACKUP_DIR/$BACKUP_FILE"
	exit 1
fi

echo ""
echo "=== Respaldo creado exitosamente ==="
echo "Archivo: $LOCAL_BACKUP_DIR/$BACKUP_FILE"
echo "Tamaño: $(ls -lh "$LOCAL_BACKUP_DIR/$BACKUP_FILE" | awk '{print $5}')"
echo ""
echo "Para restaurar:"
echo "docker exec -i $MYSQl_CONTAIMER mysql -u root -p$DB_ROOT_PASSWORD $DB_NAME < $LOCAL_BACKUP_DIR/$BACKUP_FILE"