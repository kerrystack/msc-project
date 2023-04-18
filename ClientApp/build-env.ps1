﻿# Go to terraform cluster code location
write-host "1. - Go to terraform cluster code location"
cd "C:\D\msc_project\msc-project\provision-eks-cluster"

# Terraform apply, and auto approving
write-host "2. - Terraform apply, and auto approving"
terraform apply -auto-approve

# 2. Set kube config to point to newly created cluster
write-host "3. - Set kube config to point to newly created cluster"
aws eks --region $(terraform output -raw region) update-kubeconfig --name $(terraform output -raw cluster_name)

# 4. Install prometheus with Grafana stack (ref here: https://github.com/prometheus-community/helm-charts/tree/main/charts/kube-prometheus-stack)
write-host "4. - Install prometheus with Grafana stack"
helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
helm repo update
helm install prometheus-stack prometheus-community/kube-prometheus-stack

# 5. Deploy metrics server (See https://github.com/kubernetes-sigs/metrics-server#readme)
write-host "5. - Deploy metrics server"
kubectl apply -f https://github.com/kubernetes-sigs/metrics-server/releases/latest/download/components.yaml

# 6. Deploy sample app (Setup service monitor, See https://github.com/prometheus-operator/prometheus-operator/blob/main/Documentation/user-guides/getting-started.md#deploying-a-sample-application)
write-host "6. - Deploy sample app"
cd C:\D\msc_project\msc-project\experiments
kubectl apply -f .\deployment.yml
kubectl apply -f .\service.yml
kubectl apply -f .\servicemonitor.yml

# 7. Waiting for app to be deployed
write-host "7. - Waiting for app to be deployed"
Start-Sleep -s 60

# 7. - Port forward for appropriate applications and ports
write-host "8. - Port forward for appropriate applications and ports"
start-job {kubectl port-forward deploy/prometheus-stack-grafana 8080:3000}
start-job {kubectl port-forward svc/prometheus-stack-kube-prom-prometheus 9090}
start-job {kubectl port-forward deploy/php-apache 8001:80}